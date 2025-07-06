import { MessageFlags } from 'discord.js';
import { api } from '../services/api.js';

export async function taskDeleteModal(interaction) {
    console.log('Task delete modal submitted');
    const taskId = interaction.fields.getTextInputValue('taskIdInput');

    try {
        await api.delete(`/api/Task/${taskId}`, {
            userId: interaction.user.id,
        });

        console.log('\nTask deleted successfully:', { taskId }, '\n');
        await interaction.reply({
            content: `Task ${taskId} deleted successfully!`,
            flags: MessageFlags.Ephemeral,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        console.log('\nError deleting task:', errorMessage, '\n');
        await interaction.reply({
            content: `Error deleting task: ${errorMessage}`,
            flags: MessageFlags.Ephemeral,
        });
    }
}