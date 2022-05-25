export type UnrankedPlayer = {
    id: number,
    name: string,
    image: string,
    rating: number
}

export type Player = UnrankedPlayer & {
    rank: number
}

export type Game = {
    winner: UnrankedPlayer,
    loser: UnrankedPlayer
}

const generatePlayer = (id: number, name: string) => ({
    id, name,
    image: `http://placekitten.com/300/${290 + id}`,
    rating: 560 + id * 37
})

export const generatePlayers = () : UnrankedPlayer[] => {
    return [
            generatePlayer(1, "David"),
            generatePlayer(2, "Liam H."),
            generatePlayer(3, "Maxwell"),
            generatePlayer(4, "Thea"),
            generatePlayer(5, "Harry"),
    ]
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
    const difference = game.loser.rating = game.winner.rating;
    const expected = 1 / (1 + Math.pow(10, difference/volatility))
    const exchanged = Math.round(eloFactor * (1- expected));

    const newWinner = {...game.winner, rating: game.winner.rating + exchanged};
    const newLoser = {...game.loser, rating: game.loser.rating - exchanged};

    const updatedPlayers = players.map(p => p.id === newWinner.id ? newWinner : (p.id === newLoser.id ? newLoser : p));

    return rankPlayers(updatedPlayers);
}
