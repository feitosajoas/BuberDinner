using System;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public record AuthResult(
  User User,
  string Token
);