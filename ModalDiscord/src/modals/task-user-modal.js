import { MessageFlags } from 'discord.js';
import { api } from '../services/api.js';
import { fetchUsers } from '../utils/api-utils.js';

export async function taskUserModal(interaction) {
    console.log('Task user modal submitted');
    const taskId = interaction.fields.getTextInputValue('taskIdInput');
    const responsibleId = interaction.fields.getTextInputValue('responsibleIdInput');

    // Validar se os IDs são números
    if (isNaN(taskId) || isNaN(responsibleId)) {
        console.log('\nError: Invalid task ID or user ID:', { taskId, responsibleId }, '\n');
        await interaction.reply({
            content: 'Error: Task ID and Responsible User ID must be numbers.',
            flags: MessageFlags.Ephemeral,
        });
        return;
    }

    try {
        // Validar se o ID do usuário existe
        const users = await fetchUsers(interaction.user.id);
        const userExists = users.some(user => user.id === parseInt(responsibleId));
        if (!userExists) {
            console.log('\nError: User ID not found:', responsibleId, '\n');
            await interaction.reply({
                content: 'Error: Responsible User ID does not exist.',
                flags: MessageFlags.Ephemeral,
            });
            return;
        }

        // Associar o usuário à tarefa
        await api.post(
            '/api/TaskUser',
            {
                ID_User: parseInt(responsibleId),
                ID_Task: parseInt(taskId),
            },
            { userId: interaction.user.id }
        );

        console.log('\nUser assigned to task successfully:', { taskId, responsibleId }, '\n');
        await interaction.reply({
            content: `User ID ${responsibleId} assigned to Task ID ${taskId} successfully!`,
            flags: MessageFlags.Ephemeral,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        console.log('\nError assigning user to task:', errorMessage, '\n');
        await interaction.reply({
            content: `Error assigning user to task: ${errorMessage}`,
            flags: MessageFlags.Ephemeral,
        });
    }
}