import { ExtendedClient } from './ExtendedClient.js';
import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder, InteractionType } from 'discord.js';
import { REST, Routes } from 'discord.js';
import { SlashCommandBuilder } from '@discordjs/builders';

const client = new ExtendedClient();

client.once('ready', () => {
    console.log(`Logged in as ${client.user.tag}!`);
});

client.on('interactionCreate', async interaction => {
    console.log('Interaction received:', interaction.commandName);
    if (!interaction.isChatInputCommand()) return;

    // Comando /task (existente)
    if (interaction.commandName === 'create-task') {
        console.log('Task command triggered');
        const modal = new ModalBuilder()
            .setCustomId('taskModal')
            .setTitle('Create a Task');

        const titleInput = new TextInputBuilder()
            .setCustomId('titleInput')
            .setLabel('Titulo')
            .setStyle(TextInputStyle.Short);

        const descriptionInput = new TextInputBuilder()
            .setCustomId('descriptionInput')
            .setLabel('Descricao')
            .setStyle(TextInputStyle.Paragraph);

        const creationDateInput = new TextInputBuilder()
            .setCustomId('creationDateInput')
            .setLabel('Data de Criacao')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('AAAA-MM-DD');

        const priorityInput = new TextInputBuilder()
            .setCustomId('priorityInput')
            .setLabel('Prioridade')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Ex.: Baixa, Urgente');

        const titleRow = new ActionRowBuilder().addComponents(titleInput);
        const descriptionRow = new ActionRowBuilder().addComponents(descriptionInput);
        const dateRow = new ActionRowBuilder().addComponents(creationDateInput);
        const priorityRow = new ActionRowBuilder().addComponents(priorityInput);

        modal.addComponents(titleRow, descriptionRow, dateRow, priorityRow);

        await interaction.showModal(modal);
    }

  
    if (interaction.commandName === 'login') {
        console.log('Login command triggered');
        const modal = new ModalBuilder()
            .setCustomId('loginModal')
            .setTitle('Login');

        const emailInput = new TextInputBuilder()
            .setCustomId('emailInput')
            .setLabel('Email')
            .setStyle(TextInputStyle.Short);

        const passwordInput = new TextInputBuilder()
            .setCustomId('passwordInput')
            .setLabel('Senha')
            .setStyle(TextInputStyle.Short);

        const emailRow = new ActionRowBuilder().addComponents(emailInput);
        const passwordRow = new ActionRowBuilder().addComponents(passwordInput);

        modal.addComponents(emailRow, passwordRow);

        await interaction.showModal(modal);
    }

    // Novo comando /consulta-task
    if (interaction.commandName === 'consulta-task') {
        console.log('Consulta-task command triggered');
        const modal = new ModalBuilder()
            .setCustomId('consultaTaskModal')
            .setTitle('Consultar Task');

        const taskIdInput = new TextInputBuilder()
            .setCustomId('taskIdInput')
            .setLabel('ID da Task')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Ex.: 12345');

        const taskIdRow = new ActionRowBuilder().addComponents(taskIdInput);

        modal.addComponents(taskIdRow);

        await interaction.showModal(modal);
    }

 
    if (interaction.type === InteractionType.ModalSubmit) {
        if (interaction.customId === 'loginModal') {
            console.log('Login modal submitted');
            const email = interaction.fields.getTextInputValue('emailInput');
            const password = interaction.fields.getTextInputValue('passwordInput');

            await interaction.reply({
                content: `Login submitted!\n**Email:** ${email}\n**Senha:** ${password}`,
                ephemeral: true
            });
        }

        // Submissão do modal /consulta-task (abre novo modal)
        if (interaction.customId === 'consultaTaskModal') {
            console.log('Consulta-task modal submitted');
            const taskId = interaction.fields.getTextInputValue('taskIdInput');

            // Novo modal com informações da task (layout apenas)
            const taskInfoModal = new ModalBuilder()
                .setCustomId('taskInfoModal')
                .setTitle('Task Information');

            const titleInfoInput = new TextInputBuilder()
                .setCustomId('titleInfoInput')
                .setLabel('Titulo')
                .setStyle(TextInputStyle.Short)
                .setValue('Título Exemplo'); // Placeholder

            const descriptionInfoInput = new TextInputBuilder()
                .setCustomId('descriptionInfoInput')
                .setLabel('Descricao')
                .setStyle(TextInputStyle.Paragraph)
                .setValue('Descrição Exemplo'); // Placeholder

            const dateInfoInput = new TextInputBuilder()
                .setCustomId('dateInfoInput')
                .setLabel('Data de Criacao')
                .setStyle(TextInputStyle.Short)
                .setValue('2025-06-18'); // Placeholder

            const priorityInfoInput = new TextInputBuilder()
                .setCustomId('priorityInfoInput')
                .setLabel('Prioridade')
                .setStyle(TextInputStyle.Short)
                .setValue('Média'); // Placeholder

            const titleInfoRow = new ActionRowBuilder().addComponents(titleInfoInput);
            const descriptionInfoRow = new ActionRowBuilder().addComponents(descriptionInfoInput);
            const dateInfoRow = new ActionRowBuilder().addComponents(dateInfoInput);
            const priorityInfoRow = new ActionRowBuilder().addComponents(priorityInfoInput);

            taskInfoModal.addComponents(titleInfoRow, descriptionInfoRow, dateInfoRow, priorityInfoRow);

            await interaction.showModal(taskInfoModal);
        }

     
        if (interaction.customId === 'taskModal') {
            console.log('Task modal submitted');
            const title = interaction.fields.getTextInputValue('titleInput');
            const description = interaction.fields.getTextInputValue('descriptionInput');
            const creationDate = interaction.fields.getTextInputValue('creationDateInput');
            const priority = interaction.fields.getTextInputValue('priorityInput');

            await interaction.reply({
                content: `Task created!\n**Título:** ${title}\n**Descrição:** ${description}\n**Data de Criação:** ${creationDate}\n**Prioridade:** ${priority}`,
                ephemeral: true
            });
        }
    }
});

const commands = [
    new SlashCommandBuilder()
        .setName('create-task')
        .setDescription('Creates a new task with a modal'),
    new SlashCommandBuilder()
        .setName('login')
        .setDescription('Opens a login modal'),
    new SlashCommandBuilder()
        .setName('consulta-task')
        .setDescription('Consults a task by ID')
];

const rest = new REST({ version: '10' }).setToken(process.env.TOKEN);

(async () => {
    try {
        console.log('Started refreshing application (/) commands.');
        await rest.put(Routes.applicationGuildCommands(process.env.CLIENT_ID, process.env.GUILD_ID), { body: commands });
        console.log('Successfully reloaded application (/) commands.');
    } catch (error) {
        console.error('Command registration error:', error);
    }
})();

client.start();