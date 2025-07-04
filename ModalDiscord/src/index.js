import dotenv from 'dotenv';
dotenv.config();

import { ExtendedClient } from './utils/client.js';
import { registerCommands } from './utils/command-register.js';
import { createTaskCommand } from './commands/create-task.js';
import { loginCommand } from './commands/login.js';
import { taskQueryCommand } from './commands/task-query.js';
import { taskModal } from './modals/task-modal.js';
import { loginModal } from './modals/login-modal.js';
import { taskQueryModal } from './modals/task-query-modal.js';
import { taskInfoModal } from './modals/task-info-modal.js';

const client = new ExtendedClient();

client.once('ready', () => {
    console.log(`Logged in as ${client.user.tag}!`);
});

client.on('interactionCreate', async (interaction) => {
    if (interaction.isChatInputCommand()) {
        if (interaction.commandName === 'create-task') await createTaskCommand.execute(interaction);
        if (interaction.commandName === 'login') await loginCommand.execute(interaction);
        if (interaction.commandName === 'task-query') await taskQueryCommand.execute(interaction);
    }

    if (interaction.isModalSubmit()) {
        if (interaction.customId === 'taskModal') await taskModal(interaction);
        if (interaction.customId === 'loginModal') await loginModal(interaction);
        if (interaction.customId === 'taskQueryModal') await taskQueryModal(interaction);
        if (interaction.customId === 'taskInfoModal') await taskInfoModal(interaction);
    }
});

const commands = [createTaskCommand.command, loginCommand.command, taskQueryCommand.command];
registerCommands(commands);

client.start();