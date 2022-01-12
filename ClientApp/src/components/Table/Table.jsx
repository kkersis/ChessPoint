import {useSortBy, useTable} from 'react-table'
import React from 'react'
import './Table.css'

function Table({ columns, data, getRowProps}) {
    // Use the state and functions returned from useTable to build your UI
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