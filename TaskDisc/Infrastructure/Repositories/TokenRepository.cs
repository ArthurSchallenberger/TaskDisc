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

    public async Task<TokenEntity> GetValidyHashTokenByEmail(string email)
    {
        
        return await _context.Tokens
            .Where(t => t.User.Email == email && !t.IsRevoked && t.Expiration_Date > DateTime.UtcNow)
            .OrderByDescending(t => t.Creation_Date)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<TokenEntity>> GetExpiredTokensByUserId(int id)
    {
        return await _context.Tokens
            .Where(t => t.Expiration_Date < DateTime.UtcNow & t.ID_User == id)
            .OrderByDescending(t => t.Expiration_Date)
            .ToListAsync();
    }

    public async Task<TokenEntity> GetValidyTokenByUserId(int id)
    {
        return await _context.Tokens
            .Where(t => t.ID_User == id && !t.IsRevoked && t.Expiration_Date > DateTime.UtcNow)
            .OrderByDescending(t => t.Creation_Date)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TokenExists(string token)
    {
        return await _context.Tokens.AnyAsync(t => t.Token == token);
    }

    public async Task<TokenEntity> Update(TokenEntity tokenEntity)
    {
        _context.Tokens.Update(tokenEntity);
        await _context.SaveChangesAsync();
        return tokenEntity;
    }
}
