import { Table } from 'antd';
import { useContext } from 'react';
import { CryptoContext } from '../../context/crypto-context';

const columns = [
  {
    title: 'Name',
    dataIndex: 'name',
    showSorterTooltip: { target: 'full-header' },

    sorter: (a, b) => a.name.length - b.name.length,
    sortDirections: ['descend'],
  },
  {
    title: 'Price, $',
    dataIndex: 'price',
    defaultSortOrder: 'descend',
    sorter: (a, b) => a.price - b.price,
  },
  {
    title: 'Amount',
    dataIndex: 'amount',
    defaultSortOrder: 'descend',
    sorter: (a, b) => a.amount - b.amount,
  },
  {
    title: 'Transaction date',
    dataIndex: 'transactionDate',

    sorter: (a, b) => a.transactionDate - b.transactionDate,
    onFilter: (value, record) => record.address.indexOf(value) === 0,
  },
  
];

export default function TransactionsTable(){
const {wallet, prices, transactions} = useContext(CryptoContext)

const data = transactions.map((c) => ({
    key: c.id,
    name: c.name,
    price: c.price,
    amount: c.amount,
    transactionDate: new Date(c.transactionDate).toLocaleString('uk-UA', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  }),
}))

const onChange = (pagination, filters, sorter, extra) => {
  console.log('params', pagination, filters, sorter, extra);
};
    return(
        <Table 
        pagination={false}
        columns={columns} 
        dataSource={data} 
        onChange={onChange} 
        showSorterTooltip={{ target: 'sorter-icon' }}/>
    )
}