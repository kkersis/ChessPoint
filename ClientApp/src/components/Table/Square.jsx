import React from "react";

function Square({color}){
    return <div style={{height:'20px', width:'20px', backgroundColor: color == 'balta' ? 'white' : 'black', 
                border:'2px solid black', margin:'auto'}}></div>
}

export default Square;