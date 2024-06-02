
import React, { useEffect, useState } from "react";
import BugsTable from "../components/bugs-table"
import Button from 'react-bootstrap/Button';

function Bugs() {

    const [searchParameters, setSearchParameters] = useState({ count: 10, page: 1,sort: "title" });
    const [pagedList, setPagedList] = useState([]);


    useEffect(() => {
        fetch('https://localhost:7040/Bug/_search', {
            method: "post",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(searchParameters)
        })
            .then(response => response.json())
            .then(data => { setPagedList(JSON.parse(data));})
    }, []);

    return (
        <>
        <BugsTable pagedList={pagedList} />
 

        </>
    );
}

export default Bugs;

//<Bugs pagedList={pagedList }/>
//console.log(pagedList);

