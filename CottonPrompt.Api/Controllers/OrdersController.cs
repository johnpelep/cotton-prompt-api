﻿using CottonPrompt.Api.Extensions;
using CottonPrompt.Api.Messages.Orders;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Orders;
using CottonPrompt.Infrastructure.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetOrdersRequest request)
        {
            var result = await orderService.GetAsync(request.Priority, request.ArtistStatus, request.CheckerStatus, request.CustomerStatus, request.ArtistId, request.CheckerId, request.NoArtist, request.NoChecker);
            return Ok(result);
        }

        [HttpGet("ongoing")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOngoingAsync([FromQuery] GetOngoingOrdersRequest request)
        {
            var result = await orderService.GetOngoingAsync(request.AsModel());
            return Ok(result);
        }

        [HttpGet("rejected")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRejectedAsync([FromQuery] GetRejectedOrdersRequest request)
        {
            var result = await orderService.GetRejectedAsync(request.AsModel());
            return Ok(result);
        }

        [HttpGet("completed")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCompletedAsync([FromQuery] GetCompletedOrdersRequest request)
        {
            var result = await orderService.GetCompletedAsync(request.AsModel());
            return Ok(result);
        }

        [HttpGet("reported")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportedAsync([FromQuery] GetReportedOrdersRequest request)
        {
            var result = await orderService.GetReportedAsync(request.AsModel());
            return Ok(result);
        }

        [HttpGet("sent-for-printing")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSentForPrintingAsync([FromQuery] GetSentForPrintingOrdersRequest request)
        {
            var result = await orderService.GetSentForPrintingAsync(request.AsModel());
            return Ok(result);
        }

        [HttpGet("available-as-artist")]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvailableAsArtistAsync([FromQuery] GetAvailableAsArtistOrdersRequest request)
        {
            var result = await orderService.GetAvailableAsArtistAsync(request.ArtistId, request.Priority, request.ChangeRequest);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetOrderModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await orderService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
        {
            await orderService.CreateAsync(request.AsEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await orderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderRequest request)
        {
            await orderService.UpdateAsync(request.AsEntity());
            return NoContent();
        }

        [HttpPost("{id}/artist")]
        [ProducesResponseType<CanDoModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignArtistAsync([FromRoute] int id, [FromBody] AssignArtristRequest request)
        {
            var result = await orderService.AssignArtistAsync(id, request.ArtistId);
            return Ok(result);
        }

        [HttpPost("{id}/checker")]
        [ProducesResponseType<CanDoModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignCheckerAsync([FromRoute] int id, [FromBody] AssignCheckerRequest request)
        {
            var result = await orderService.AssignCheckerAsync(id, request.CheckerId);
            return Ok(result);
        }

        [HttpPost("{id}/designs")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> SubmitDesignAsync([FromRoute] int id, [FromBody] SubmitDesignRequest request)
        {
            await orderService.SubmitDesignAsync(id, request.FileName, request.Design);
            return NoContent();
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ApproveAsync([FromRoute] int id)
        {
            await orderService.ApproveAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/accept")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AcceptAsync([FromRoute] int id)
        {
            await orderService.AcceptAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/change-request")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangeRequestAsync([FromRoute] int id, [FromBody] ChangeRequestRequest request)
        {
            await orderService.ChangeRequestAsync(id, request.DesignId, request.Comment, request.ImageReferences.AsEntity(Guid.Empty, id));
            return NoContent();
        }

        [HttpGet("{id}/download")]
        [ProducesResponseType<FileResult>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DownloadAsync([FromRoute] int id)
        {
            var result = await orderService.DownloadAsync(id);
            return File(result.Content, result.ContentType, result.FileName);
        }

        [HttpPost("{id}/resend-for-customer-review")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ResendForCustomerReviewAsync([FromRoute] int id)
        {
            await orderService.ResendForCustomerReviewAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/report")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ReportAsync([FromRoute] int id, [FromBody] ReportRequest request)
        {
            await orderService.ReportAsync(id, request.Reason, request.IsRedraw);
            return NoContent();
        }

        [HttpPost("{id}/resolve")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ResolveAsync([FromRoute] int id, [FromBody] ResolveRequest request)
        {
            await orderService.ResolveAsync(id, request.ResolvedBy);
            return NoContent();
        }

        [HttpPost("{id}/send-for-printing")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> SendForPrintingAsync([FromRoute] int id, [FromBody] SendForPrintingRequest request)
        {
            await orderService.SendForPrintingAsync(id, request.UserId);
            return NoContent();
        }

        [HttpPost("{changeRequestOrderId}/redraw")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RedrawAsync([FromRoute] int changeRequestOrderId, [FromBody] CreateOrderRequest request)
        {
            await orderService.RedrawAsync(request.AsEntity(), changeRequestOrderId);
            return NoContent();
        }
    }
}
