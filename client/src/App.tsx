import React, {useState} from 'react';
import {PlayerGrid} from "./components/PlayerGrid";
import {generatePlayers, Player, rankPlayers, updateWithGame} from "./models/Player";

function App() {
  const [players, setPlayers] = useState(rankPlayers(generatePlayers(50)));
  const [player1, setPlayer1] = useState<Player | null>(null);
  const [player2, setPlayer2] = useState<Player | null>(null);
  
  const onSelectPlayer = (player: Player) => {
      if (player === player1) {
          setPlayer1(null);
      } else if (player === player2) {
          setPlayer2(null);
      } else if (player1 === null) {
          setPlayer1(player);
      } else {
          setPlayer2(player);
      }
  }
  
  return (
    <div className="App">
      <PlayerGrid players={players} player1={player1} player2={player2} onSelectPlayer={onSelectPlayer}/>
    </div>
  );
}

export default App;
