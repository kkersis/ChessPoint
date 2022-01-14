import React from 'react'
import ChesscomLogo from '../Shared Icons/ChesscomLogo'
import LichessLogo from '../Shared Icons/LichessLogo'
import "./ShowGamesFormStyles.css"

function ShowGamesForm({onSubmit}) {
    return (
        <div>
            <form onSubmit={onSubmit}>
                <input placeholder='Chess.com username' className='mb-2 w-25' name='chesscomInput'/>
                <ChesscomLogo width='25px' height='25px'/>
                <br/>
                <input placeholder='Lichess.org username' className='mb-2 w-25' name='lichessInput'/>
                <LichessLogo width='25px' height='25px'/>
                <br/>
                <button className='mb-2 w-25' type='submit'>Show</button>
            </form>
        </div>
    )
}

export default ShowGamesForm
