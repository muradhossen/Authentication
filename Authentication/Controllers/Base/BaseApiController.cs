using Application.DTOs;
using Application.Services.Accounts;
using Domain.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase { }
