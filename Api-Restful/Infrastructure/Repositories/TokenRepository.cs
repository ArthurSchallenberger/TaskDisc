using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using Discord;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly DatabaseContext _context;
    public TokenRepository(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<TokenEntity> Add(TokenEntity tokenEntity)
    {
        await _context.Tokens.AddAsync(tokenEntity);
        await _context.SaveChangesAsync();
        return tokenEntity;
    }

    public async Task<bool> Delete(int id)
    {
        var token = _context.Tokens.Find(id);
        if (token == null) return false;
        _context.Tokens.Remove(token);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TokenEntity> GetByHashToken(string hashToken)
    {
        return await _context.Tokens.FirstOrDefaultAsync(t => t.Token == hashToken);
    }

    public async Task<TokenEntity> Update(TokenEntity tokenEntity)
    {
        _context.Tokens.Update(tokenEntity);
        await _context.SaveChangesAsync();
        return tokenEntity;
    }
}
