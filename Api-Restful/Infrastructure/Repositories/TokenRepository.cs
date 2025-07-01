using TaskDisc.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskDisc.Infrastructure.Repositories;

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

    public async Task<ICollection<TokenEntity>> GetExpiredTokensByUserId(int id)
    {
        return await _context.Tokens
            .Where(t => t.Expiration_Date < DateTime.UtcNow & t.ID_User == id)
            .OrderByDescending(t => t.Expiration_Date)
            .ToListAsync();
    }

    public async Task<TokenEntity> Update(TokenEntity tokenEntity)
    {
        _context.Tokens.Update(tokenEntity);
        await _context.SaveChangesAsync();
        return tokenEntity;
    }
}
