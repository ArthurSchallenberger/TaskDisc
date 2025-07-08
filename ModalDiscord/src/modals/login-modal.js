import { api } from '../services/api.js';


const userTokens = {};

export async function loginModal(interaction) {
    console.log('Login modal submitted');
    const email = interaction.fields.getTextInputValue('emailInput');
    const password = interaction.fields.getTextInputValue('passwordInput');

    try {
        console.log(process.env.API_URL);
        const response = await api.post('/api/Auth/token', { email, password });
        console.log(response);
        
        const token = response.data.token;
        userTokens[interaction.user.id] = token;

        await interaction.reply({
            content: `Login successful!\n**Email:** ${email}`,
            ephemeral: true,
        });
    } catch (error) {
        const errorMessage = error.response?.data?.message || error.message;
        await interaction.reply({
            content: `Error during login: ${errorMessage}`,
            ephemeral: true,
        });
    }
}

export { userTokens }; 