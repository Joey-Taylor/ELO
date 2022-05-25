import React from 'react';
import {PlayerGrid} from "./components/PlayerGrid";
import {generatePlayers, rankPlayers} from "./models/Player";

function App() {
  const players = rankPlayers(generatePlayers(50));
  return (
    <div className="App">
      <PlayerGrid players={players}/>
    </div>
  );
}

export default App;
