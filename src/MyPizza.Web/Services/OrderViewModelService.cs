using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MimeKit;
using MyPizza.ApplicationCore.Entities;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;
using System.Collections.Generic;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MyPizza.Web.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderViewModelService> _logger;
        private readonly IEmailSender _emailSender;
        [Obsolete]
        private IHostingEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Obsolete]
        public OrderViewModelService(IUoWRepository repository,
                                    IMapper mapper,
                                    ILogger<OrderViewModelService> logger,
                                    IEmailSender emailSender,
                                    IHostingEnvironment env,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _emailSender = emailSender;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OrderViewModel> GetLastUserOrderAsync(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} got his last order");
            var order = await _repository.Orders.FirstOrDefaultAsync(
                predicate: x => x.UserId == userId,
                orderBy: x => x.OrderByDescending(x => x.Date));
            if (order == null)
            {
                return new OrderViewModel() { UserId = userId};
            }

            var orderModel = _mapper.Map<OrderViewModel>(order);
            return orderModel;
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderViewModel model)
        {
            _logger.LogInformation($"User with id {model.UserId} created an order");
            var newOrder = _mapper.Map<Order>(model);
            newOrder = await _repository.Orders.CreateAsync(newOrder);
            var newModel = _mapper.Map<OrderViewModel>(newOrder);
            return newModel;
        }

        public async Task<IList<OrderViewModel>> GetUserOrdersAsync(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} got orders list");
            var orders = await _repository.Orders.GetListAsync(
                predicate: x => x.UserId == userId,
                orderBy: x => x.OrderByDescending(x => x.Date),
                includes: x => x.Include(p => p.Products)
                                .ThenInclude(x => x.Product)!
                                .ThenInclude(x => x!.ProductType!)
                                .ThenInclude(x => x.Category!));
            if (orders == null)
            {
                return new List<OrderViewModel>();
            }

            var orderModels = _mapper.Map<List<OrderViewModel>>(orders);
            return orderModels;
        }

        public async Task SendOrderConfirmationEmailAsync(OrderViewModel model)
        {
             var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "OrderTest.html";

            var builder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string productsHtml = BuildHtmlStringForProductList(model.Products);
            string messageBody = string.Format(builder.HtmlBody,
                        model.Email,
                        model.Sum(),
                        model.Date,
                        productsHtml);

            await _emailSender.SendEmailAsync(model.Email!, "Order confirmation", messageBody);
        }

        private string BuildHtmlStringForProductList(List<OrderProductViewModel> products)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            foreach (var product in products)
            {
                htmlBuilder.Append("<tr>");
                    htmlBuilder.Append("<td align=\" left\" valign=\"middle\" width=\"20%\">");
                        htmlBuilder.Append($"<img src=\"{_httpContextAccessor.HttpContext!.Request.Scheme}" +
                                           $"://{_httpContextAccessor.HttpContext!.Request.Host}/" +
                                           $"{product.PictureUrl}\" alt=\"img\" width=\"50%\">");
                    htmlBuilder.Append("</td>");
                    htmlBuilder.Append("<td align=\" left\" valign=\"middle\" width=\"20%\">");
                        htmlBuilder.Append(product.Name);
                    htmlBuilder.Append("</td>");
                    htmlBuilder.Append("<td align=\" left\" valign=\"middle\" width=\"20%\">");
                        htmlBuilder.Append("&#36;" + product.Price);
                    htmlBuilder.Append("</td>");
                    htmlBuilder.Append("<td align=\" left\" valign=\"middle\" width=\"20%\">");
                        htmlBuilder.Append($"{product.Weight}&nbsp;{product.Measure}");
                    htmlBuilder.Append("</td>");
                    htmlBuilder.Append("<td align=\" left\" valign=\"middle\" width=\"20%\">");
                        htmlBuilder.Append(product.Quantity);
                    htmlBuilder.Append("</td>");
                htmlBuilder.Append("</tr>");
            }
            return htmlBuilder.ToString();
        }
    }
}
