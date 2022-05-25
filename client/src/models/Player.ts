export type UnrankedPlayer = {
    readonly id: number,
    readonly name: string,
    readonly image: string,
    readonly rating: number
}

export type Player = UnrankedPlayer & {
    readonly rank: number
}

export type Game = {
    readonly winner: UnrankedPlayer,
    readonly loser: UnrankedPlayer
}

const forenames = ["David", "Thea", "Max", "Liam", "Harry", "Sam", "Alex"]
const initials = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "W"];

const generateName = (id: number) => { return `${forenames[id % forenames.length]} ${initials[id % initials.length]}.` };

const generatePlayer = (id: number, max: number) => ({
    id, name: generateName(id),
    image: `http://placekitten.com/300/${300 - max / 2 + id}`,
    rating: 1000 - (max * 37 / 2) + id * 37
});

export const generatePlayers = (count: number) : UnrankedPlayer[] => {
    return Array.from(Array(count)).map((n, id) => generatePlayer(id, count));
}

const comp = (left: number, right: number) => Number(left > right) - Number(left < right);

export const rankPlayers = (players: UnrankedPlayer[]) : Player[] => {
    const sorted = players.slice().sort((p1, p2) => comp(p2.rating, p1.rating) || comp(p1.id, p2.id));
    let currentRank = 1;
    let nextRank = 1;
    let currentRating = 0;

    const rankedPlayers : Player[] = [];

    for (const player of sorted) {
        if (player.rating !== currentRating) {
            currentRating = player.rating;
            currentRank = nextRank;
        }

        const rankedPlayer : Player = { ...player, rank: currentRank }
        rankedPlayers.push(rankedPlayer);

        nextRank++;
    }

    return rankedPlayers;
}

const volatility = 400;
const eloFactor = 50;

export const updateWithGame = (players: UnrankedPlayer[], game: Game): Player[] => {
    const difference = game.loser.rating - game.winner.rating;
    const expected = 1 / (1 + Math.pow(10, difference/volatility))
    const exchanged = Math.round(eloFactor * (1- expected));

    const newWinner = {...game.winner, rating: game.winner.rating + exchanged};
    const newLoser = {...game.loser, rating: game.loser.rating - exchanged};

    const updatedPlayers = players.map(p => p.id === newWinner.id ? newWinner : (p.id === newLoser.id ? newLoser : p));

    return rankPlayers(updatedPlayers);
}
