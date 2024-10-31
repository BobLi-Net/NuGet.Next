
import { Empty, List } from 'antd';
import { memo, useEffect, useState } from "react";

import { Flexbox } from 'react-layout-kit';
import PackageItem from './PackageItem';
import { Search } from '@/services/SearchService';
import { useQuery } from '@/hooks/useQuery';

const PackageList = memo(() => {
    const [packages, setPackages] = useState([]);
    const [loading, setLoading] = useState(true);
    const query = useQuery();


    async function loadPackages() {
        setLoading(true);
        try {
            const result = await Search(query.q, 0, 20, query.prerelease, null, query.packageType, query.packageType);
            setPackages(result.data);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        loadPackages();
    }, [query.q, query.prerelease, query.packageType]);


    if (packages.length === 0) {
        return <Flexbox style={{
            justifyContent: 'center',
            alignItems: 'center',
            height: '100%',
            fontSize: 20,
        }}>
            <Empty
                description="没有找到相关包"
            />
        </Flexbox>
    }

    return (<>
        <List style={{
            height: '100%',
            width: '100%',
        }} dataSource={packages} loading={loading} renderItem={(item: any) => <PackageItem key={item.id} packageItem={item} />} />
    </>)
})

PackageList.displayName = 'Packages'

export default PackageList;