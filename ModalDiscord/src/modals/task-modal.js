import { MessageFlags } from 'discord.js';
import { api } from '../services/api.js';

export async function taskModal(interaction) {
    console.log('Task modal submitted');
    const subject = interaction.fields.getTextInputValue('subjectInput');
    const description = interaction.fields.getTextInputValue('descriptionInput');
    const priority = interaction.fields.getTextInputValue('priorityInput');
    const completion_Date = interaction.fields.getTextInputValue('completionDateInput')
    const status = interaction.fields.getTextInputValue('statusInput');

    try {
        const response = await api.post(
            '/api/task',
            {
                subject,
                description,
                priority: parseInt(priority),
                Completion_Date: new Date(completion_Date),
                status,
            },
            { userId: interaction.user.id }
        );

        console.log('\nTask created successfully:', { subject, description, priority, completion_Date, status }, '\n');
        await interaction.reply({
            content: `Task created!\n**Subject:** ${subject}\n**Description:** ${description}\n**Priority:** ${priority}\n**Status:** ${status}`,
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