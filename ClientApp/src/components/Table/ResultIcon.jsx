import React from 'react'
import {FaEquals, FaMinusCircle, FaPlusCircle} from 'react-icons/fa'
import { IconContext } from 'react-icons'

function ResultIcon({result}) {
    let icon;
    let iconColor;
    if(result < 0){
        icon = <FaMinusCircle />
        iconColor = 'red';
    }else if(result > 0){
        icon = <FaPlusCircle />
        iconColor = 'green';
    }else{
        icon = <FaEquals />;
        iconColor = 'gray';
    }
    return (
        <IconContext.Provider value={{color:iconColor}}>
            <div>
                {icon}
            </div>
        </IconContext.Provider>
    )
}

export default ResultIcon
