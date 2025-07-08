import { MessageFlags } from 'discord.js';
import { api } from '../services/api.js';

export async function taskModal(interaction) {
    console.log('Task modal submitted');
    const subject = interaction.fields.getTextInputValue('subjectInput');
    const description = interaction.fields.getTextInputValue('descriptionInput');
    const priority = interaction.fields.getTextInputValue('priorityInput');
    const status = interaction.fields.getTextInputValue('statusInput');
    const completionDate = interaction.fields.getTextInputValue('completionDateInput');

    try {
        // Criar a tarefa
        const taskData = {
            subject,
            description,
            priority: parseInt(priority),
            status,
            completion_Date: new Date(completionDate).toISOString(), // Converte para ISO
        };

        const taskResponse = await api.post('/api/Task', taskData, { userId: interaction.user.id });
        const taskId = taskResponse.data.id;

        console.log('\nTask created successfully:', { subject, description, priority, status, completionDate, taskId }, '\n');
        await interaction.reply({
            content: `Task created!\n**Subject:** ${subject}\n**Description:** ${description}\n**Priority:** ${priority}\n**Status:** ${status}\n**Completion Date:** ${completionDate}\n**Task ID:** ${taskId}\nUse /assign-task-user to assign a user.`,
            flags: MessageFlags.Ephemeral,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        console.log('\nError creating task:', errorMessage, '\n');
        await interaction.reply({
            content: `Error creating task: ${errorMessage}`,
            flags: MessageFlags.Ephemeral,
        });
    }
}