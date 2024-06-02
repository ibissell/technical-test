import React from 'react'
import Table from 'react-bootstrap/Table';
//import Row from 'react-bootstrap/Row';
//import Col from 'react-bootstrap/Col';
//import Pagination from 'react-bootstrap/Pagination';
//import React from 'react'
import BugRow from './bug-row'

function BugsTable({ pagedList }) {
    console.log(pagedList);
    return (
     
        <Table hover>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Priority</th>
                    <th>Status</th>
                    <th>Assignee</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {
     
                    pagedList.items?.map((bug) =>
                    <BugRow key={ bug.BugId } bug={bug} />
                )}
   
            </tbody>
        </Table>
    );
}

export default BugsTable;