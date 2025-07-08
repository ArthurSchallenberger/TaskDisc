import { SlashCommandBuilder, ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder, MessageFlags } from 'discord.js';
import { fetchUsers } from '../utils/api-utils.js';

export const assignTaskUserCommand = {
    command: new SlashCommandBuilder()
        .setName('assign-task-user')
        .setDescription('Assign a user to a task'),
    async execute(interaction) {
        console.log('Assign task user command triggered');

        try {
            const users = await fetchUsers(interaction.user.id);
            if (!users || users.length === 0) {
                await interaction.reply({
                    content: 'No users found. Please try again later.',
                    flags: MessageFlags.Ephemeral,
                });
                return;
            }

            const userList = users.slice(0, 5).map(user => `${user.name} (ID: ${user.id})`).join(', ') + (users.length > 5 ? '...' : '');

            const modal = new ModalBuilder()
                .setCustomId('taskUserModal')
                .setTitle('Assign User to Task');

            const taskIdInput = new TextInputBuilder()
                .setCustomId('taskIdInput')
                .setLabel('Task ID')
                .setStyle(TextInputStyle.Short)
                .setPlaceholder('Enter the task ID')
                .setRequired(true);

            const responsibleIdInput = new TextInputBuilder()
                .setCustomId('responsibleIdInput')
                .setLabel('Responsible User ID')
                .setStyle(TextInputStyle.Short)
                .setPlaceholder(`Enter user ID: ${userList}`)
                .setRequired(true);

            const taskIdRow = new ActionRowBuilder().addComponents(taskIdInput);
            const responsibleRow = new ActionRowBuilder().addComponents(responsibleIdInput);

            modal.addComponents(taskIdRow, responsibleRow);

            await interaction.showModal(modal);
        } catch (error) {
            console.log('\nError creating task user modal:', error.message, '\n');
            await interaction.reply({
                content: `Error preparing task user assignment: ${error.message}`,
                flags: MessageFlags.Ephemeral,
            });
        }
    },
};