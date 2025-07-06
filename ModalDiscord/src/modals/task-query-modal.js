import { MessageFlags, ActionRowBuilder, ButtonBuilder, ButtonStyle } from 'discord.js';
import { api } from '../services/api.js';

// Cache temporário para armazenar dados da tarefa
const taskDataCache = {};

export async function taskQueryModal(interaction) {
    console.log('Task query modal submitted');
    const taskId = interaction.fields.getTextInputValue('taskIdInput');

    try {
        const response = await api.get(`/api/Task/${taskId}`, {
            userId: interaction.user.id,
        });
        const task = response.data;

        const cacheKey = `${interaction.user.id}-${taskId}`;
        taskDataCache[cacheKey] = {
            subject: task.subject || 'N/A',
            description: task.description || 'N/A',
            creation_Date: task.creation_Date ? new Date(task.creation_Date).toLocaleString('pt-BR', { timeZone: 'America/Sao_Paulo' }) : 'N/A',
            priority: task.priority?.toString() || 'N/A',
            status: task.status || 'N/A',
            taskId: taskId, 
        };

        console.log('\nTask data cached:', taskDataCache[cacheKey], '\n');

        const button = new ButtonBuilder()
            .setCustomId(`show_task_info_${cacheKey}`)
            .setLabel('View Task Details')
            .setStyle(ButtonStyle.Primary);

        const row = new ActionRowBuilder().addComponents(button);

        await interaction.reply({
            content: `Task found! Click the button to view details.`,
            components: [row],
            flags: MessageFlags.Ephemeral,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        console.log('\nError querying task:', errorMessage, '\n');
        await interaction.reply({
            content: `Error querying task: ${errorMessage}`,
            flags: MessageFlags.Ephemeral,
        });
    }
}

export { taskDataCache };