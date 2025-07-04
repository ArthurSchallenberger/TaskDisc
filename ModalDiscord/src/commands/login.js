import { SlashCommandBuilder } from '@discordjs/builders';
import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder } from 'discord.js';

export const loginCommand = {
    command: new SlashCommandBuilder()
        .setName('login')
        .setDescription('Opens a login modal'),

    async execute(interaction) {
        console.log('Login command triggered');
        const modal = new ModalBuilder()
            .setCustomId('loginModal')
            .setTitle('Login');

        const emailInput = new TextInputBuilder()
            .setCustomId('emailInput')
            .setLabel('Email')
            .setStyle(TextInputStyle.Short);

        const passwordInput = new TextInputBuilder()
            .setCustomId('passwordInput')
            .setLabel('Senha')
            .setStyle(TextInputStyle.Short);

        const emailRow = new ActionRowBuilder().addComponents(emailInput);
        const passwordRow = new ActionRowBuilder().addComponents(passwordInput);

        modal.addComponents(emailRow, passwordRow);

        await interaction.showModal(modal);
    },
};