import React, { Component } from 'react';
import { useTable, ReactTable } from 'react-table';
import Table from './Table/Table';
import Square from './Table/Square';
import ResultIcon from './Table/ResultIcon';
import ChesscomLogo from './Table/ChesscomLogo';
import LichessLogo from './Table/LichessLogo';
import BulletIcon from './Table/BulletIcon';
import RapidIcon from './Table/RapidIcon';
import BlitzIcon from './Table/BlitzIcon';

export class AllGames extends Component {
    static displayName = AllGames.name;

    constructor(props) {
        super(props);
        this.state = { games: [], loading: true };
    }

    componentDidMount() {
        this.FetchAllGames();
    }

    renderGames() {
        // return this.state.games.map((game, index) => (
        //     <div style={{backgroundColor: this.getBgColor(game)}}>
        //         {index+1}{' '}
        //         {game.serverType === "CHESS.COM" ? "CHESS.COM " : "LICHESS " }
        //         <a href={game.url}>{new Date(game.date).toLocaleDateString()}</a>{' '}
        //         {game.opening.name}
        //         {game.me.username}
        //         {game.opponent.username}
        //         {game.ending}
        //     </div>
        // ))
        console.log(this.state.games)
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
        console.log(renderableGames)
        const columns = [
                {
                    Cell: timeClassCell =>{
                        if(timeClassCell.value == "bullet") return <BulletIcon />
                        else if(timeClassCell.value == "blitz") return <BlitzIcon />
                        else if(timeClassCell.value == "rapid") return <RapidIcon />
                        else return "?";
                    },
                    Header: 'Speed',
                    accessor: 'timeClass'
                },
                {
                    Cell: serverCell => {
                        if(serverCell.value == 0) return <ChesscomLogo />
                        else if(serverCell.value == 1) return <LichessLogo />
                        else return 'UNKNOWN'
                    },
                    Header: 'Server',
                    accessor: 'server'
                },
                {
                    Header: 'Date',
                    accessor: 'date'
                },
                {
                    Header: 'Opening',
                    accessor: 'openingName'
                },
                {
                    Header: 'Opponent',
                    accessor: 'opponentUsername'
                },
                {         
                    Cell: color => color.value == 0 ? <Square color='balta' /> : <Square color='juoda' />,
                    Header: 'Color',
                    accessor: 'color'
                },
                {
                    Cell: resultCell => <ResultIcon result={resultCell.value} />,
                    Header: 'Result',
                    accessor: 'result'
                },
            ]

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
                        columns={columns}
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

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderGames()

        return (
            <div>
                <h1 id="tabelLabel" >All games</h1>
                {contents}
            </div>
        );
    }

    async FetchAllGames() {
        try{
            //const response = await fetch(process.env.REACT_APP_API_URL + "game/nealkoholins/loki55/games");
            const response = await fetch(process.env.REACT_APP_API_URL + "game/spalio29/kkersis/games");
            const data = await response.json();
            this.setState({ games: data, loading: false });
        }
        catch(err){
            console.log(err);
        }
    }
}
