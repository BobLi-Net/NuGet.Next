import { get } from "@/utils/fetch"


export const GetPackageVersions = (id:string) => {
    return get(`/v3/package/${id}/index.json`)
}

export const DownloadPackage = (id:string) => {
    return get(`/v3/package/${id}/index.json`)
}