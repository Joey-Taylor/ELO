import React, {useState} from 'react';
import {Game, generatePlayers, Player, rankPlayers, updateWithGame} from "../models/Player";
import {PlayerSelect} from "./PlayerSelect";
import {WinnerSelect} from "./WinnerSelect";

export const PlayerSelectAndGame = () => {
  const [players, setPlayers] = useState(rankPlayers(generatePlayers(50)));
  const [player1, setPlayer1] = useState<Player | null>(null);
  const [player2, setPlayer2] = useState<Player | null>(null);

    const onSelectPlayer = (player: Player) => {
        if (player === player1) {
            setPlayer1(null);
        } else if (player1 === null) {
            setPlayer1(player);
        } else {
            setPlayer2(player);
        }
    }
    
    const clearSelectedPlayers = () => {
        setPlayer1(null);
        setPlayer2(null);
    }
    
    const onCompleteGame = (game: Game) => {
        clearSelectedPlayers();
        
        setPlayers(updateWithGame(players, game))
    }


    return (<>
      <PlayerSelect players={players} selectedPlayer={player1} onSelectPlayer={onSelectPlayer}/>
      <WinnerSelect player1={player1} player2={player2} onComplete={onCompleteGame} onCancel={clearSelectedPlayers}/>
    </>
  );
}
