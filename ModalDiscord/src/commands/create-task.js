import { SlashCommandBuilder } from '@discordjs/builders';
import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder } from 'discord.js';

export const createTaskCommand = {
    command: new SlashCommandBuilder()
        .setName('create-task')
        .setDescription('Creates a new task with a modal'),

    async execute(interaction) {
        console.log('Task command triggered');
        const modal = new ModalBuilder()
            .setCustomId('taskModal')
            .setTitle('Create a Task');

        const titleInput = new TextInputBuilder()
            .setCustomId('titleInput')
            .setLabel('Titulo')
            .setStyle(TextInputStyle.Short);

        const descriptionInput = new TextInputBuilder()
            .setCustomId('descriptionInput')
            .setLabel('Descricao')
            .setStyle(TextInputStyle.Paragraph);

        const creationDateInput = new TextInputBuilder()
            .setCustomId('creationDateInput')
            .setLabel('Data de Criacao')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('AAAA-MM-DD');

        const priorityInput = new TextInputBuilder()
            .setCustomId('priorityInput')
            .setLabel('Prioridade')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Ex.: Baixa, Urgente');

        const titleRow = new ActionRowBuilder().addComponents(titleInput);
        const descriptionRow = new ActionRowBuilder().addComponents(descriptionInput);
        const dateRow = new ActionRowBuilder().addComponents(creationDateInput);
        const priorityRow = new ActionRowBuilder().addComponents(priorityInput);

        modal.addComponents(titleRow, descriptionRow, dateRow, priorityRow);

        await interaction.showModal(modal);
    },
};