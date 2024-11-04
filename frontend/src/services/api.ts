//un get request to sign up 




export const getTokenAsync = async (getAccessToken: () => Promise<string>) => {
    try {
        const token = await getAccessToken();
        return token;
    } catch (err) {
        throw new Error('Failed to get access token');
    }
};
export const getToken = async (token: string) => {
    try {
        const response = await fetch('https://localhost:7200/api/GetToken', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage || 'An error occurred while fetching the token.');
        }

        const emailResponse = await response.text();
        return emailResponse;
    } catch (error) {
        throw new Error('An unexpected error occurred.');
    }
};

export const fetchUserId = async (email: string, token: string, URL: string) => {
    const url = `${URL}/Users/email_${email}`;
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            throw new Error('Failed to fetch user ID');
        }

        const json = await response.json();
        return {
            userId: json.user_id,
            teamId: json.team_id,
            isTeamLeader: json.is_team_leader,
        };
    } catch (err) {
        throw new Error((err as Error).message);
    }
};

export const fetchUserName = async (id: number, token: string, URL: string) => {
    const url = `${URL}/Users/id_${id}`;
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            throw new Error('Failed to fetch user ID');
        }

        const json = await response.json();
        return {
            userName: json.name,
        };
    } catch (err) {
        throw new Error((err as Error).message);
    }
};

export const PutDailyTime = async (
    userId: string | null,
    token: string,
    inputValue: string,
): Promise<string> => {
    const formattedTime = inputValue.trim();
    const timeData = { daily_time: formattedTime };
    console.log(formattedTime);
    console.log('Time data being sent: ', JSON.stringify(timeData));
    try {
        const response = await fetch(`https://localhost:7200/api/Teams/${userId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(timeData),
        });

        if (response.ok) {
            await response.json();
            console.log(response);
            return 'Echipa a fost actualizată cu succes!';
        } else if (response.status === 204) {
            return 'Nu s-a găsit echipa.';
        } else {
            const errorMessage = await response.text();
            return `A apărut o eroare: ${errorMessage}`;
        }
    } catch (error) {
        console.error('Error updating team:', error);
        return 'A apărut o eroare la actualizarea echipei.';
    }
};

export const PostJoke = async (
    joke: string,
    userId: string | null,
    teamId: string | null,
    token: string,
): Promise<string> => {
    const currentDate = new Date().toISOString();
    const jokeData = {
        content: joke,
        author_id: userId,
        team_id: teamId,
        post_date: currentDate,
    };

    try {
        const response = await fetch(`https://localhost:7200/api/Jokes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(jokeData),
        });

        if (response.ok) {
            await response.json();
            return 'The joke was successfully added.';
        } else if (response.status === 404) {
            return 'No team found';
        } else {
            const errorMessage = await response.text();
            return `An error has occurred. ${errorMessage}`;
        }
    } catch (error) {
        console.error('Error adding joke:', error);
        return 'An error has occurred while entering the joke. ';
    }
};

export interface Joke {
    joke_id: number;
    author_id: number;
    user: any;
    team_id: number;
    team: any;
    content: string;
    post_date: string;
}

export const getJokes = async (token: string, teamId: string): Promise<Joke[]> => {
    try {
        const response = await fetch(`https://localhost:7200/api/Jokes/${teamId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage || 'An error occurred while fetching the jokes.');
        }

        const jokeResponse: Joke[] = await response.json();
        return jokeResponse;
    } catch (error) {
        throw new Error('An unexpected error occurred');
    }
};

export interface JokeWithAuthor {
    content: string;
    postDate: string;
    authorName: string;
    authorId: number;
}

export const getJokesWithAuthors = async (
    token: string,
    teamId: string,
    URL: string,
): Promise<JokeWithAuthor[]> => {
    try {
        const jokes: Joke[] = await getJokes(token, teamId);

        const jokesWithAuthors: JokeWithAuthor[] = await Promise.all(
            jokes.map(async (joke) => {
                const { userName } = await fetchUserName(joke.author_id, token, URL);
                return {
                    content: joke.content,
                    postDate: joke.post_date,
                    authorName: userName,
                    authorId: joke.author_id,
                };
            }),
        );

        return jokesWithAuthors;
    } catch (error) {
        throw new Error('An error occurred while fetching jokes with authors');
    }
};

export const makeAuthorizedRequest = async (token: string) => {
    const apiEndpoint = 'https://localhost:7200/api/UpdateDatabase';

    try {
        const response = await fetch(apiEndpoint, {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response;
    } catch (error) {
        console.error('Error making request:', error);
        throw new Error('A apărut o eroare la actualizarea bazei de date.');
    }
};

export const getDailyTime = async (teamId: string | null, token: string) => {
    try {
        const response = await fetch(`https://localhost:7200/api/Teams/${teamId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage || 'An error occurred while fetching the token.');
        }

        const emailResponse = await response.text();
        return emailResponse;
    } catch (error) {
        throw new Error('An unexpected error occurred.');
    }
};

export const fetchAuthorId = async (teamId: string | null, token: string, URL: string) => {
    const url = `${URL}/Users/turn_${teamId}`;
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            throw new Error('Failed to fetch user ID');
        }

        const json = await response.json();
        console.log(json);
        return json;
    } catch (err) {
        throw new Error((err as Error).message);
    }
};

export const GetUserIdByTeamId = async (teamId: string | null, token: string) => {
    try {
        const response = await fetch(`https://localhost:7200/api/Users/${teamId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage || 'An error occurred while fetching the token.');
        }

        const responseAPi = await response.text();
        return responseAPi;
    } catch (error) {
        throw new Error('An unexpected error occurred.');
    }
};

//disertatie
//sign-up 
export const SignUpUser = async (
    userEmail: string,
    password: string,
    token: string,
): Promise<string> => {
    const userData = {
        UserEmail: userEmail,
        PasswordHash: password,
    };

    try {
        const response = await fetch(`https://localhost:5001/api/authentication/sign-up`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(userData),
        });

        if (response.ok) {
            const createdUser = await response.json();
            return `User successfully created.`;
        } else if (response.status === 400) {
            const errorMessage = await response.text();
            return `${errorMessage}`;
        } else {
            const errorMessage = await response.text();
            return `An unexpected error has occurred. ${errorMessage}`;
        }
    } catch (error) {
        console.error('Error signing up user:', error);
        return 'An error has occurred while signing up the user.';
    }
};

//sign-in
export const SignInUser = async (
    userEmail: string,
    password: string,
    token: string,
): Promise<string> => {
    const userData = {
        UserEmail: userEmail,
        PasswordHash: password,
    };

    try {
        const response = await fetch(`https://localhost:5001/api/authentication/sign-in`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(userData),
        });

        if (response.ok) {
            const authResponse = await response.json();
            return `User successfully signed in. Token: ${authResponse.token}`; 
        } else if (response.status === 401) {
            const errorMessage = await response.text();
            return `Sign-in failed: ${errorMessage}`;
        } else {
            const errorMessage = await response.text();
            return `An unexpected error has occurred. ${errorMessage}`;
        }
    } catch (error) {
        console.error('Error signing in user:', error);
        return 'An error has occurred while signing in the user.';
    }
};
