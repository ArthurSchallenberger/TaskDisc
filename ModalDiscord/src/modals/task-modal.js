import { api } from '../services/api.js';

export async function taskModal(interaction) {
    console.log('Task modal submitted');
    const title = interaction.fields.getTextInputValue('titleInput');
    const description = interaction.fields.getTextInputValue('descriptionInput');
    const creationDate = interaction.fields.getTextInputValue('creationDateInput');
    const priority = interaction.fields.getTextInputValue('priorityInput');

    try {
        // Requisição à API com userId para incluir o JWT
        const response = await api.post(
            '/tasks',
            {
                title,
                description,
                creationDate,
                priority,
            },
            { userId: interaction.user.id }
        );

        await interaction.reply({
            content: `Task created!\n**Title:** ${title}\n**Description:** ${description}\n**Creation Date:** ${creationDate}\n**Priority:** ${priority}`,
            ephemeral: true,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        await interaction.reply({
            content: `Error creating task: ${errorMessage}`,
            ephemeral: true,
        });
    }
}