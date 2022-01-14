import React, { Component } from 'react';
import Table from './GamesTable/GamesTable';
import ShowGamesForm from './Show Games Form/ShowGamesForm';

export class AllGames extends Component {
    static displayName = AllGames.name;

    constructor(props) {
        super(props);
        this.state = { games: [], loading: true, showGames: false, 
            chesscomInput: '', lichessInput: ''};
    }

    componentDidMount() {
        this.FetchAllGames();
    }

    renderGames() {
        let renderableGames = this.state.games.map((game, index) => (
            {
                timeClass: game.timeClass,
                server: game.server,
                date: new Date(game.date).toLocaleDateString('lt'),
                openingName: game.opening.name,
                opponentUsername: game.opponent.username,
                color: game.myColor,
                result: game.ending,
                url: game.url
            }
        ))

        function getRowBgColor(row){
            if(row.original.result == -1){
                return "#ffe3e3";
            }else if(row.original.result == 1){
                return "#e8ffea";
            }else return '#f7f7f7';
        }
        
        return (
                <Table 
                    data={renderableGames}
                    getRowProps={row => ({
                        style: {
                        background: getRowBgColor(row),
                        },
                    })}
                />
        )
    }

    getBgColor(game){
        if(game.ending == 1)
            return 'green';
        else if(game.ending == -1)
            return 'red';
        else
            return 'gray';
    }

    handleSubmit = (event) =>{
        event.preventDefault();
        this.setState({chesscomInput: event.target.chesscomInput.value,
                       lichessInput: event.target.lichessInput.value,
                       showGames: true, loading:true}, this.FetchAllGames);
    }

    render() {
        let allGames;
        if(this.state.showGames){
            allGames = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderGames()
        }

        return (
            <div>
                <h1 id="tabelLabel" >All games</h1>
                <ShowGamesForm onSubmit={this.handleSubmit}/>
                {allGames}
            </div>
        );
    }

    async FetchAllGames() {
        try{
            const response = await fetch(process.env.REACT_APP_API_URL + `game/${this.state.chesscomInput}/${this.state.lichessInput}/games`);
            const data = await response.json();
            this.setState({ games: data, loading: false });
        }
        catch(err){
            console.log(err);
        }
    }
}
