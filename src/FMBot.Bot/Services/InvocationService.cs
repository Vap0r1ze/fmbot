using System;
using Discord;
using FMBot.Bot.Models;
using FMBot.Domain.Models;
using FMBot.Persistence.EntityFrameWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FMBot.Bot.Services
{
    public class InvocationService
    {
        private readonly IDbContextFactory<FMBotDbContext> _contextFactory;
        private readonly IMemoryCache _cache;
        private readonly BotSettings _botSettings;

        public InvocationService(IDbContextFactory<FMBotDbContext> contextFactory, IMemoryCache cache, IOptions<BotSettings> botSettings)
        {
            this._contextFactory = contextFactory;
            this._cache = cache;
            this._botSettings = botSettings.Value;
        }

        public InvocationContext SetContext(IMessage message, InvocationContext context, TimeSpan ttl = default(TimeSpan)) {
            if (ttl == default(TimeSpan))
            {
                ttl = TimeSpan.FromMinutes(10);
            }
            return this._cache.Set($"context-{message.Id}", context, ttl);
        }

        public InvocationContext GetContext(IMessage message) {
            return this._cache.Get($"context-{message.Id}") as InvocationContext;
        }

        public void RemoveContext(IMessage message) {
            this._cache.Remove($"context-{message.Id}");
        }
    }
}
