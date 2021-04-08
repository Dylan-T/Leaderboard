import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { leaderboard: [], loading: true };
  }

  componentDidMount() {
    this.populateLeaderboardData();
  }

  static renderLeaderboard(leaderboard) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Player</th>
            <th>Rank</th>
          </tr>
        </thead>
        <tbody>
          {leaderboard.map(entry =>
            <tr key={entry.player}>
              <td>{entry.player}</td>
              <td>{entry.rank}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderLeaderboard(this.state.leaderboard);

    return (
      <div>
        <h1 id="tabelLabel" >Leaderboard</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateLeaderboardData() {
    const response = await fetch('leaderboard');
    console.log(response);
    const data = await response.json();
    this.setState({ leaderboard: data, loading: false });
  }
}
