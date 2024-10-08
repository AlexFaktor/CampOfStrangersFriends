﻿using Core.DatabaseRecords.Users;
using Database.Service.Users;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Service.Keyboards;

namespace TelegramBot.Service;

public class BotAnswer
{
    private readonly IServiceProvider _serviceProvider;

    private readonly UserRepository _userRepository;
    private readonly UTelegramRepository _telegramRepository;
    private readonly UStatisticsRepository _statisticsRepository;
    private readonly UResourcesRepository _resourcesRepository;

    public BotAnswer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var scope = _serviceProvider.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        _telegramRepository = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
        _statisticsRepository = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();
        _resourcesRepository = scope.ServiceProvider.GetRequiredService<UResourcesRepository>();
    }

    // Commamd "/profile" answer msg profile
    public async Task CommandProfile(ITelegramBotClient bot, Message message, GameUser user)
    {
        var statistic = await _statisticsRepository.GetAsync(user.Id);
        var resoursec = await _resourcesRepository.GetAsync(user.Id);
        await bot.SendTextMessageAsync(message.Chat.Id,
            $"User data:\n" +
            $"Username - {user.Username}\n" +
            $"Hashtag - {user.Hashtag}\n",
            replyMarkup: InlineKeyboards.GetProfile());
    }
}
