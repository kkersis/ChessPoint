import {useSortBy, useTable} from 'react-table'
import React from 'react'
import './Table.css'
import Square from './Square';
import ResultIcon from './ResultIcon';
import ChesscomLogo from '../Shared Icons/ChesscomLogo';
import LichessLogo from '../Shared Icons/LichessLogo';
import BulletIcon from './BulletIcon';
import RapidIcon from './RapidIcon';
import BlitzIcon from './BlitzIcon';

function Table({data, getRowProps}) {

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
                if(serverCell.value == 0) return <ChesscomLogo width='30px' height='30px' />
                else if(serverCell.value == 1) return <LichessLogo width='30px' height='30px'/>
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
    
    const {
      getTableProps,
      getTableBodyProps,
      headerGroups,
      rows,
      prepareRow,
    } = useTable({
      columns,
      data,
      },
      useSortBy
    )

    const handleRowClick = (row) => {
      window.open(row.original.url, "_blank");
    }
 
    // Render the UI for your table
    return (
      <table {...getTableProps()}>
        <thead>
          {headerGroups.map(headerGroup => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map(column => (
                <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                {column.render('Header')}
                  <span>
                    {column.isSorted
                      ? column.isSortedDesc
                        ? ' 🔽'
                        : ' 🔼'
                      : ''}
                  </span>
                </th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps()}>
          {rows.map((row, i) => {
            prepareRow(row)
            return (
              <tr onClick={() => handleRowClick(row)} {...row.getRowProps(getRowProps(row))}>
                  {row.cells.map(cell => {
                    return <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                  })}
              </tr>
            )
          })}
        </tbody>
      </table>
    )
  }

export default Table