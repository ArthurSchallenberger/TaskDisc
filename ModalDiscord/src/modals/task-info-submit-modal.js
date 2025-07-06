import { MessageFlags } from 'discord.js';
import { api } from '../services/api.js';
import { taskDataCache } from './task-query-modal.js';

export async function taskInfoSubmitModal(interaction) {
    console.log('Task info modal submitted');
    const cacheKey = interaction.customId.replace('taskInfoModal_', '');
    const task = taskDataCache[cacheKey];

    if (!task) {
        console.log('\nError: Task data not found in cache for key:', cacheKey, '\n');
        await interaction.reply({
            content: 'Error: Task data not found. Please try querying the task again.',
            flags: MessageFlags.Ephemeral,
        });
        return;
    }

    const taskId = task.taskId;
    const subject = interaction.fields.getTextInputValue('subjectInfoInput');
    const description = interaction.fields.getTextInputValue('descriptionInfoInput');
    const priority = interaction.fields.getTextInputValue('priorityInfoInput');
    const status = interaction.fields.getTextInputValue('statusInfoInput');

    try {
        await api.put(
            `/api/Task/${taskId}`,
            {
                subject,
                description,
                priority: parseInt(priority),
                status,
            },
            { userId: interaction.user.id }
        );

        console.log('\nTask updated successfully:', { subject, description, priority, status }, '\n');
        await interaction.reply({
            content: `Task updated!\n**Subject:** ${subject}\n**Description:** ${description}\n**Priority:** ${priority}\n**Status:** ${status}`,
            flags: MessageFlags.Ephemeral,
        });

        // Limpar o cache após a atualização
        delete taskDataCache[cacheKey];
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        console.log('\nError updating task:', errorMessage, '\n');
        await interaction.reply({
            content: `Error updating task: ${errorMessage}`,
            flags: MessageFlags.Ephemeral,
        });
    }
}