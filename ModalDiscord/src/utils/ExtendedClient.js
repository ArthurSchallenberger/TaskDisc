import { Client, Partials, IntentsBitField } from 'discord.js';

export class ExtendedClient extends Client {
    constructor() {
        super({
            intents: [
                IntentsBitField.Flags.Guilds,
                IntentsBitField.Flags.GuildMessages,
                IntentsBitField.Flags.GuildMembers,
                IntentsBitField.Flags.MessageContent,
            ],
            partials: [
                Partials.User,
                Partials.Channel,
                Partials.Message
            ]
        });
    }

    async start() {
        try {
            if (!process.env.TOKEN) {
                throw new Error('TOKEN environment variable is not defined');
            }
            await this.login(process.env.TOKEN);
            console.log('Bot logged in successfully');
        } catch (error) {
            console.error('Failed to login:', error.message);
            process.exit(1);
        }
    }
}