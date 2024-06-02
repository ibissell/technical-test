import React from 'react'

function BugRow({ bug: { BugId, Title, Status, Priority, AssignedPersonId } }) {
    return (
        <tr key={BugId}>
            <td>{Title}</td>
            <td>{Status}</td>
            <td>{Priority}</td>
            <td>{AssignedPersonId}</td>
            <td></td>
        </tr>
    )
}

export default BugRow