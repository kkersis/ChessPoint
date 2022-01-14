import React from 'react'
import chesscomlogo from '../../assets/chesscomlogo.png'

function ChesscomLogo({width, height}) {
    return (
        <img style={{width: width, height: height}} src={chesscomlogo} title='chess.com'/>
    )
}

export default ChesscomLogo
