﻿using Ecommerce.Dtos.Followers;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/following")]
    [ApiController]
    public class FollowingController(IFollowing _followingService) : ControllerBase
    {
        private readonly IFollowing followingService = _followingService;
        [HttpGet]
        [Route("followers/{userId}")]
        public async Task<IActionResult> GetFollowers([FromRoute] string userId)
        {
            var followers = await followingService.GetFollowers(userId);
            var follower = followers.Select(f => f.ToFollowerDto());
            return Ok(follower);
        }


        [HttpGet]
        [Route("{followerId}")]
        public async Task<IActionResult> GetFollowings([FromRoute] string followerId)
        {
            var followings = await followingService.GetFollowings(followerId);
            var following = followings.Select(f => f.ToFollowingDto());
            return Ok(following);
        }


        [HttpPost]
        public async Task<IActionResult> FollowUser([FromBody] FollowDto dto)
        {
            try
            {
                await followingService.FollowUser(dto);
                return Created();
            }catch(ValidationException e)
            {
                return BadRequest(
                    new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
