import { SlashCommandBuilder, ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder, MessageFlags } from 'discord.js';

export const createTaskCommand = {
    command: new SlashCommandBuilder()
        .setName('create-task')
        .setDescription('Create a new task'),
    async execute(interaction) {
        console.log('Create task command triggered');

        try {
            const modal = new ModalBuilder()
                .setCustomId('taskModal')
                .setTitle('Create Task');

            const subjectInput = new TextInputBuilder()
                .setCustomId('subjectInput')
                .setLabel('Subject')
                .setStyle(TextInputStyle.Short)
                .setRequired(true);

            const descriptionInput = new TextInputBuilder()
                .setCustomId('descriptionInput')
                .setLabel('Description')
                .setStyle(TextInputStyle.Paragraph)
                .setRequired(true);

            const priorityInput = new TextInputBuilder()
                .setCustomId('priorityInput')
                .setLabel('Priority')
                .setStyle(TextInputStyle.Short)
                .setPlaceholder('Ex.: 1, 2, 3 (higher is more urgent)')
                .setRequired(true);

            const completionDateInput = new TextInputBuilder()
                .setCustomId('completionDateInput')
                .setLabel('Completion Date')
                .setStyle(TextInputStyle.Short)
                .setPlaceholder('DD/MM/YYYY')
                .setRequired(true);

            const statusInput = new TextInputBuilder()
                .setCustomId('statusInput')
                .setLabel('Status')
                .setStyle(TextInputStyle.Short)
                .setPlaceholder('Ex.: Pending, In Progress, Completed')
                .setRequired(true);

            const subjectRow = new ActionRowBuilder().addComponents(subjectInput);
            const descriptionRow = new ActionRowBuilder().addComponents(descriptionInput);
            const priorityRow = new ActionRowBuilder().addComponents(priorityInput);
            const completionDateRow = new ActionRowBuilder().addComponents(completionDateInput);
            const statusRow = new ActionRowBuilder().addComponents(statusInput);

            modal.addComponents(subjectRow, descriptionRow, priorityRow, completionDateRow, statusRow);

            await interaction.showModal(modal);
        } catch (error) {
            console.log('\nError creating task modal:', error.message, '\n');
            await interaction.reply({
                content: `Error preparing task creation: ${error.message}`,
                flags: MessageFlags.Ephemeral,
            });
        }
    },
};