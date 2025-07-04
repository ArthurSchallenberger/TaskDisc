import { SlashCommandBuilder } from '@discordjs/builders';
import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder } from 'discord.js';

export const taskQueryCommand = {
    command: new SlashCommandBuilder()
        .setName('task-query')
        .setDescription('Queries a task by ID'),

    async execute(interaction) {
        console.log('Task-query command triggered');
        const modal = new ModalBuilder()
            .setCustomId('taskQueryModal')
            .setTitle('Query Task');

        const taskIdInput = new TextInputBuilder()
            .setCustomId('taskIdInput')
            .setLabel('Task ID')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Ex.: 12345');

        const taskIdRow = new ActionRowBuilder().addComponents(taskIdInput);

        modal.addComponents(taskIdRow);

        await interaction.showModal(modal);
    },
};