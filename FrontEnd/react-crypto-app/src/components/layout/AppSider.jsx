import { Flex, Layout } from 'antd';
import { ArrowDownOutlined, ArrowUpOutlined } from '@ant-design/icons';
import { useContext, useEffect, useState } from "react";
import { Card, Statistic, List, Typography} from 'antd';
import { percentDifference } from '../../utils'
import { CryptoContext } from '../../context/crypto-context';

const siderStyle = {
  textAlign: 'center',
  lineHeight: '120px',
  color: '#fff',
  backgroundColor: '#1677ff',
  padding: '0.5rem'
};

export default function AppSider(){
const {prices, wallet, loading} = useContext(CryptoContext)

    // return(
    //     <Layout.Sider width="25%" style={siderStyle}>
    //       <h2>Price list on top 5 COINS (USDT):</h2>
    //         {loading ? (
    //     <p>Loading...</p>
    //   ) : (
    //     <ul>
    //       {prices.map(p => {
    //         const formatted = new Intl.NumberFormat('de-DE', {
    //             minimumFractionDigits: 2, 
    //             maximumFractionDigits: 2
    //         }).format(p.price);
    //         return (
    //           <li key={p.symbol}>
    //             {p.symbol}: {formatted} USDT
    //           </li>
    //         );
    //       })}
    //     </ul>
    //   )}
    //     </Layout.Sider>
    // )



    // return(
    //     <Layout.Sider width="25%" style={siderStyle}>
    //         <Card style={{ width: '100%', marginBottom: '1rem'}}>
    //             <Statistic title="Active"
    //               value={11.28}
    //               precision={2}
    //               prefix={<ArrowUpOutlined />}
    //               valueStyle={{ color: '#3f8600' }}
    //               suffix="%"/>
    //       <List
    //         size='small'
    //         dataSource={wallet}
    //         renderItem={coin => (
    //           <List.Item>
    //             <Typography.Text strong>{coin.symbol}</Typography.Text>: {(coin.totalProfit).toFixed(2)}
    //           </List.Item>
    //         )}/>
    //         </Card>

    //         <Card style={{ width: '100%'}}>
    //             <Statistic
    //       title="Idle"
    //       value={9.3}
    //       precision={2}
    //       valueStyle={{ color: '#cf1322' }}
    //       prefix={<ArrowDownOutlined />}
    //       suffix="%"
    //     />
    //         </Card>
    //     </Layout.Sider>
    // )

    return(
      <Layout.Sider width="15%" style={siderStyle}>
        {wallet.length === 0 ? (
      <Card style={{ width: '100%', marginBottom: '1rem' }}>
        <Typography.Text strong>
          У вас пока нет криптовалюты. Купите крипту 🪙
        </Typography.Text>
      </Card>
    ) : (
      wallet.map(coin => (
        <Card key={coin.symbol} style={{ width: '100%', marginBottom: '1rem' }}>
          <Statistic 
            title={coin.symbol}
            value={coin.growPercent}
            precision={2}
            prefix={coin.grow ? <ArrowUpOutlined /> : <ArrowDownOutlined />}
            valueStyle={coin.grow ? { color: '#3f8600' } : { color: '#cf1322' }}
            suffix="%"
          />
          <List
            size='small'
            dataSource={[
              {title: 'Total Profit', value: coin.totalProfit, isDollar: true},
              {title: 'Amount', value: coin.amount},
              {title: 'Total Money', value: coin.totalMoney, isDollar: true},
            ]}
            renderItem={item => (
              <List.Item>
                <Typography.Text strong>{item.title}:</Typography.Text>
                {/* {(coin.value).toFixed(2)} */}
                
                {item.isDollar && (<Typography.Text type={coin.grow ? 'success' : 'danger'}>{(item.value).toFixed(2)}$</Typography.Text>)}
                {!item.isDollar && <span>{(item.value).toFixed(2)}</span>}
              </List.Item>
            )}/>
        </Card>
      ))
    )}
      </Layout.Sider>
    )
}