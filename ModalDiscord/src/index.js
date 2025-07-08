import dotenv from 'dotenv';
dotenv.config();

import { ExtendedClient } from './utils/client.js';
import { registerCommands } from './utils/command-register.js';
import { createTaskCommand } from './commands/create-task.js';
import { loginCommand } from './commands/login.js';
import { taskQueryCommand } from './commands/task-query.js';
import { taskDeleteCommand } from './commands/task-delete.js';
import { assignTaskUserCommand } from './commands/assign-task-user.js';
import { taskModal } from './modals/task-modal.js';
import { loginModal } from './modals/login-modal.js';
import { taskQueryModal, taskDataCache } from './modals/task-query-modal.js';
import { taskInfoModal } from './modals/task-info-modal.js';
import { taskInfoSubmitModal } from './modals/task-info-submit-modal.js';
import { taskDeleteModal } from './modals/task-delete-modal.js';
import { taskUserModal } from './modals/task-user-modal.js';

const client = new ExtendedClient();

client.once('ready', () => {
    console.log(`Logged in as ${client.user.tag}!`);
});

client.on('interactionCreate', async (interaction) => {
    if (interaction.isChatInputCommand()) {
        if (interaction.commandName === 'create-task') await createTaskCommand.execute(interaction);
        if (interaction.commandName === 'login') await loginCommand.execute(interaction);
        if (interaction.commandName === 'task-query') await taskQueryCommand.execute(interaction);
        if (interaction.commandName === 'task-delete') await taskDeleteCommand.execute(interaction);
        if (interaction.commandName === 'assign-task-user') await assignTaskUserCommand.execute(interaction);
    } else if (interaction.isModalSubmit()) {
        if (interaction.customId === 'taskModal') await taskModal(interaction);
        if (interaction.customId === 'loginModal') await loginModal(interaction);
        if (interaction.customId === 'taskQueryModal') await taskQueryModal(interaction);
        if (interaction.customId.startsWith('taskInfoModal_')) await taskInfoSubmitModal(interaction);
        if (interaction.customId === 'taskDeleteModal') await taskDeleteModal(interaction);
        if (interaction.customId === 'taskUserModal') await taskUserModal(interaction);
    } else if (interaction.isButton()) {
        if (interaction.customId.startsWith('show_task_info_')) await taskInfoModal(interaction);
    }
});

const commands = [createTaskCommand.command, loginCommand.command, taskQueryCommand.command, taskDeleteCommand.command, assignTaskUserCommand.command];
registerCommands(commands);

client.start();