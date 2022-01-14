import React, { useState } from 'react'
import ChesscomLogo from '../Shared Icons/ChesscomLogo'
import LichessLogo from '../Shared Icons/LichessLogo'
import "./ShowGamesFormStyles.css"

function ShowGamesForm({onSubmitSuccess}) {
    const [errorMsg, setErrorMsg] = useState('');

    const onSubmit = (e) =>{
        e.preventDefault();
        let chesscomInput = e.target.chesscomInput.value;
        let lichessInput = e.target.lichessInput.value;
        if(formIsValid(chesscomInput, lichessInput)){
            setErrorMsg("");
            onSubmitSuccess(chesscomInput, lichessInput);
        }
    }

    const formIsValid = (chessComInput, lichessInput) => {
        if(chessComInput === '' && lichessInput === ''){
            setErrorMsg("Both inputs can't be empty!");
            return false;
        }
        return true;
    }

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
                <p className='text-danger'>{errorMsg}</p>
            </form>
        </div>
    )
}

export default ShowGamesForm
