import { REST, Routes } from 'discord.js';

export async function registerCommands(commands) {
    const rest = new REST({ version: '10' }).setToken(process.env.TOKEN);

    try {
        console.log('Started refreshing application (/) commands.');
        await rest.put(
            Routes.applicationGuildCommands(process.env.CLIENT_ID, process.env.GUILD_ID),
            { body: commands.map((cmd) => cmd.toJSON()) }
        );
        console.log('Successfully reloaded application (/) commands.');
    } catch (error) {
        console.error('Command registration error:', error);
    }
}