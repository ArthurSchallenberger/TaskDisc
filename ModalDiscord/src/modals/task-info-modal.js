import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder, MessageFlags } from 'discord.js';
import { taskDataCache } from './task-query-modal.js';

export async function taskInfoModal(interaction) {
    console.log('Task info modal triggered');
    const cacheKey = interaction.customId.replace('show_task_info_', '');
    const task = taskDataCache[cacheKey];

    if (!task) {
        console.log('\nError: Task data not found in cache for key:', cacheKey, '\n');
        await interaction.reply({
            content: 'Error: Task data not found. Please try querying the task again.',
            flags: MessageFlags.Ephemeral,
        });
        return;
    }

    const taskInfoModal = new ModalBuilder()
        .setCustomId(`taskInfoModal_${cacheKey}`)
        .setTitle('Task Information');

    const subjectInfoInput = new TextInputBuilder()
        .setCustomId('subjectInfoInput')
        .setLabel('Subject')
        .setStyle(TextInputStyle.Short)
        .setValue(task.subject);

    const descriptionInfoInput = new TextInputBuilder()
        .setCustomId('descriptionInfoInput')
        .setLabel('Description')
        .setStyle(TextInputStyle.Paragraph)
        .setValue(task.description);

    const dateInfoInput = new TextInputBuilder()
        .setCustomId('dateInfoInput')
        .setLabel('Creation Date')
        .setStyle(TextInputStyle.Short)
        .setValue(task.creation_Date)
        .setRequired(false);

    const priorityInfoInput = new TextInputBuilder()
        .setCustomId('priorityInfoInput')
        .setLabel('Priority')
        .setStyle(TextInputStyle.Short)
        .setValue(task.priority);

    const statusInfoInput = new TextInputBuilder()
        .setCustomId('statusInfoInput')
        .setLabel('Status')
        .setStyle(TextInputStyle.Short)
        .setValue(task.status);

    const subjectInfoRow = new ActionRowBuilder().addComponents(subjectInfoInput);
    const descriptionInfoRow = new ActionRowBuilder().addComponents(descriptionInfoInput);
    const dateInfoRow = new ActionRowBuilder().addComponents(dateInfoInput);
    const priorityInfoRow = new ActionRowBuilder().addComponents(priorityInfoInput);
    const statusInfoRow = new ActionRowBuilder().addComponents(statusInfoInput);

    taskInfoModal.addComponents(subjectInfoRow, descriptionInfoRow, dateInfoRow, priorityInfoRow, statusInfoRow);

    await interaction.showModal(taskInfoModal);
}