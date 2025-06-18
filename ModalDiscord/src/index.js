import { ExtendedClient } from './ExtendedClient.js';
import { ModalBuilder, TextInputBuilder, TextInputStyle, ActionRowBuilder, InteractionType } from 'discord.js';
import { REST, Routes } from 'discord.js';
import { SlashCommandBuilder } from '@discordjs/builders';

const client = new ExtendedClient();

client.once('ready', () => {
    console.log(`Logged in as ${client.user.tag}!`);
});

client.on('interactionCreate', async interaction => {
    console.log('Interaction received:', interaction.commandName); // Depura��o
    if (!interaction.isChatInputCommand()) return;

    if (interaction.commandName === 'task') {
        console.log('Task command triggered'); // Depura��o
        const modal = new ModalBuilder()
            .setCustomId('taskModal')
            .setTitle('Create a Task');

        // Campo T�tulo
        const titleInput = new TextInputBuilder()
            .setCustomId('titleInput')
            .setLabel('T�tulo')
            .setStyle(TextInputStyle.Short);

        // Campo Descri��o
        const descriptionInput = new TextInputBuilder()
            .setCustomId('descriptionInput')
            .setLabel('Descri��o')
            .setStyle(TextInputStyle.Paragraph);

        // Campo Data de Cria��o
        const creationDateInput = new TextInputBuilder()
            .setCustomId('creationDateInput')
            .setLabel('Data de Cria��o')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('AAAA-MM-DD');

        // Campo Prioridade (substitu�do por texto curto)
        const priorityInput = new TextInputBuilder()
            .setCustomId('priorityInput')
            .setLabel('Prioridade')
            .setStyle(TextInputStyle.Short)
            .setPlaceholder('Ex.: Baixa, M�dia, Alta');

        const titleRow = new ActionRowBuilder().addComponents(titleInput);
        const descriptionRow = new ActionRowBuilder().addComponents(descriptionInput);
        const dateRow = new ActionRowBuilder().addComponents(creationDateInput);
        const priorityRow = new ActionRowBuilder().addComponents(priorityInput);

        modal.addComponents(titleRow, descriptionRow, dateRow, priorityRow);

        await interaction.showModal(modal);
    }

    if (interaction.type === InteractionType.ModalSubmit) {
        if (interaction.customId === 'taskModal') {
            console.log('Task modal submitted'); // Depura��o
            const title = interaction.fields.getTextInputValue('titleInput');
            const description = interaction.fields.getTextInputValue('descriptionInput');
            const creationDate = interaction.fields.getTextInputValue('creationDateInput');
            const priority = interaction.fields.getTextInputValue('priorityInput');

            await interaction.reply({
                content: `Task created!\n**T�tulo:** ${title}\n**Descri��o:** ${description}\n**Data de Cria��o:** ${creationDate}\n**Prioridade:** ${priority}`,
                ephemeral: true
            });
        }
    }
});

const commands = [
    new SlashCommandBuilder()
        .setName('task')
        .setDescription('Creates a new task with a modal')
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