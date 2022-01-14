import React from 'react'
import lichesslogo from '../../assets/lichesslogo.png'

function LichessLogo({width, height}) {
    return (
        <img style={{width: width, height: height}} src={lichesslogo} title='lichess.org'/>
    )
}

export default LichessLogo
