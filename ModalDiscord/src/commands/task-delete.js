import { SlashCommandBuilder, ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder } from 'discord.js';

export const taskDeleteCommand = {
    command: new SlashCommandBuilder()
        .setName('task-delete')
        .setDescription('Delete a task by ID'),
    async execute(interaction) {
        console.log('Task delete command triggered');
        const modal = new ModalBuilder()
            .setCustomId('taskDeleteModal')
            .setTitle('Delete Task');

        const taskIdInput = new TextInputBuilder()
            .setCustomId('taskIdInput')
            .setLabel('Task ID')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Enter the task ID')
            .setRequired(true);

        const taskIdRow = new ActionRowBuilder().addComponents(taskIdInput);
        modal.addComponents(taskIdRow);

        await interaction.showModal(modal);
    },
};