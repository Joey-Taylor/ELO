export const player1Highlight = "red";
export const player1Background = "darkred";

export const player2Highlight = "blue";
export const player2Background = "darkblue";

export const winnerBackground = "gold";

export const firstPlaceRankBackground = "gold";
export const secondPlaceRankBackground = "silver";
export const thirdPlaceRankBackground = "#CD7F32";
export const forthAndBeyondPlaceRankBackground = "green";

export const transparent = "transparent";
export const white = "white";
export const transparentWhite = "rgba(255,255,255,0.2)";


export const backgroundForRank = (rank: number) => {
    switch(rank) {
        case 1:
            return firstPlaceRankBackground;
        case 2:
            return secondPlaceRankBackground;
        case 3:
            return thirdPlaceRankBackground;
        default:
            return forthAndBeyondPlaceRankBackground;
    }
}
