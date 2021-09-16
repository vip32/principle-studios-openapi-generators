import allOperations from "./no-refs/operations";
import { conversion as lookupRecord, Responses as LookupResponses } from "./no-refs/operations/lookupRecord";
import { RequestConversion, RequestBodies, RequestConversions, TransformRequest, StandardResponse, TransformCallType } from "~/src/types";
import { RequestOpts, ResponseArgs } from "~/src/inputs-outputs";

function fetchRequest<TParams extends {}, TBody extends RequestBodies, TResponse extends StandardResponse, TCallType extends TransformCallType>(
    conversion: RequestConversion<TParams, TBody, TResponse, TCallType>
): TransformRequest<TParams, TBody, TCallType, Promise<TResponse>> {

    function transform(params: TParams): Promise<TResponse>;
    function transform<K extends keyof TBody>(params: TParams, body: TBody[K], mimeType: K): Promise<TResponse>;
    async function transform(params: TParams, body: TBody[any] | null = null, mimeType: string | null = null): Promise<TResponse> {
        const requestOpts: RequestOpts = conversion.request(params, body!, mimeType!);
        const response: ResponseArgs = await Promise.reject<ResponseArgs>(); // This is just a demo that always fails
        return conversion.response(response);
    }
    return transform as TransformRequest<TParams, TBody, TCallType, Promise<TResponse>>;
}
const fetchLookupRecord = fetchRequest(lookupRecord);
async function testPipe() {
    const response: LookupResponses = await fetchLookupRecord({}, { formattedAddress: '123 Main St', location: { latitude: 0, longitude: 0 } }, 'application/json');
}

type Converted<TConversion extends RequestConversion<any, any, any, any>> = TConversion extends RequestConversion<infer TParams, infer TBody, infer TResponse, infer TCallType> ? TransformRequest<TParams, TBody, TCallType, Promise<TResponse>> : never;

function applyTransform<TMethods extends RequestConversions>(
    methods: TMethods,
    transform: (input: RequestConversion<any, any, any, any>) => Converted<RequestConversion<any, any, any, any>>
): {
        [K in keyof TMethods]: Converted<TMethods[K]>;
    } {
    return Object.keys(methods).reduce((prev, next) => ({ ...prev, [next]: transform(methods[next]) }), {} as {
        [K in keyof TMethods]: Converted<TMethods[K]>;
    });
}
const fetchTemp = applyTransform(allOperations, fetchRequest);
