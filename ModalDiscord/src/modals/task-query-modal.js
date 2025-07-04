import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder } from 'discord.js';
import { api } from '../services/api.js';

export async function taskQueryModal(interaction) {
    console.log('Task query modal submitted');
    const taskId = interaction.fields.getTextInputValue('taskIdInput');

    try {
        // Requisição à API com userId para incluir o JWT
        const response = await api.get(`/tasks/${taskId}`, {
            userId: interaction.user.id,
        });
        const task = response.data;

        // Modal com informações da task
        const taskInfoModal = new ModalBuilder()
            .setCustomId('taskInfoModal')
            .setTitle('Task Information');

        const titleInfoInput = new TextInputBuilder()
            .setCustomId('titleInfoInput')
            .setLabel('Title')
            .setStyle(TextInputStyle.Short)
            .setValue(task.title || 'N/A');

        const descriptionInfoInput = new TextInputBuilder()
            .setCustomId('descriptionInfoInput')
            .setLabel('Description')
            .setStyle(TextInputStyle.Paragraph)
            .setValue(task.description || 'N/A');

        const dateInfoInput = new TextInputBuilder()
            .setCustomId('dateInfoInput')
            .setLabel('Creation Date')
            .setStyle(TextInputStyle.Short)
            .setValue(task.creationDate || 'N/A');

        const priorityInfoInput = new TextInputBuilder()
            .setCustomId('priorityInfoInput')
            .setLabel('Priority')
            .setStyle(TextInputStyle.Short)
            .setValue(task.priority || 'N/A');

        const titleInfoRow = new ActionRowBuilder().addComponents(titleInfoInput);
        const descriptionInfoRow = new ActionRowBuilder().addComponents(descriptionInfoInput);
        const dateInfoRow = new ActionRowBuilder().addComponents(dateInfoInput);
        const priorityInfoRow = new ActionRowBuilder().addComponents(priorityInfoInput);

        taskInfoModal.addComponents(titleInfoRow, descriptionInfoRow, dateInfoRow, priorityInfoRow);

        await interaction.showModal(taskInfoModal);
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        await interaction.reply({
            content: `Error querying task: ${errorMessage}`,
            ephemeral: true,
        });
    }
}